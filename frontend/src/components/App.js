import { Route, Switch, useHistory, Redirect, useLocation } from 'react-router-dom';
import React, { Component, useEffect, useState } from 'react';
import {CurrentUserContext} from "../contexts/CurrentUserContext";
import useEscapePress from '../hooks/useEscapePress';
import Header from './Header';
import Main from './Main';
import Footer from './Footer';
import Register from './Register';
import Login from './Login';
import Profile from './Profile';
import NotFound from './NotFound';
import ProtectedRoute from './ProtectedRoute';
import Preloader from './Preloader';
import StatusInfoBlock from './StatusInfoBlock';
import auth from '../utils/Authentification';
import api from '../utils/Api';
import Educators from './Educators';
import {ErrorBoundary} from 'react-error-boundary'

function MyFallbackComponent({error, resetErrorBoundary}) {
  return (
    <div role="alert">
      <p>Something went wrong:</p>
      <pre>{error.message}</pre>
      <button onClick={resetErrorBoundary}>Try again</button>
    </div>
  )
}

function App() {
  const history = useHistory();
  const [isBurgerMenuOpened, setIsBurgerMenuOpened] = useState(false);
  const location = useLocation();
  const [loggedIn, setLoggedIn] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const [completeLoad, setCompleteLoad] = useState(false);
  const [isStatusInfo, setIsStatusInfo] = useState({
    isOpen: false,
    successful: true,
    text: ''
  });

  const [currentUser, setCurrentUser] = useState({});

  const headerEndpoints = ['/', '/educators', '/profile'];
  const footerEndpoints = ['/', '/educators'];

  const onClickBurgerMenu = () => {
    setIsBurgerMenuOpened(!isBurgerMenuOpened);
  }

  const returnOnPrevPage = () => {
    history.goBack();
  }

  function handleCloseStatusInfo() {
    setIsStatusInfo({...isStatusInfo, isOpen: false});
  }

  function handleLogin({ email, password }) {
    setIsLoading(true);
    auth
      .login(email, password)
      .then(jwt => {
        if(jwt.Token) {
          localStorage.setItem('jwt', jwt.Token);
          setLoggedIn(true);
          history.push('/educators');
          setIsStatusInfo({
            isOpen: true,
            successful: true,
            text: "Добро пожаловать!",
          })
        }
      })
      .catch(error => {
        setIsStatusInfo({
          isOpen: true,
          successful:false,
          text: error,
        })
      })
      .finally(() => setIsLoading(false));
  }

  function handleRegister({ name, email, password }) {
    setIsLoading(true);
    auth
      .register(name, email, password)
      .then(data => {
        if(data) {
          handleLogin({ email, password });
          history.push('/educators');
          setIsStatusInfo({
            isOpen: true,
            successful: true,
            text: "Вы зарегистрированы!",
          })
        }
      })
      .catch(error =>
        setIsStatusInfo({
          isOpen: true,
          successful:false,
          text: error,
        })
      )
      .finally(() => setIsLoading(false));
  }

  function handleSignOut() {
    localStorage.removeItem('jwt');
    setLoggedIn(false);
    history.push('/');
    setCurrentUser({});
    setLoggedIn(false);
  }

  function handleEditProfile({ name, email }) {
    setIsLoading(true);
    auth
      .setUserInfo(name, email, currentUser.UserId)
      .then(newData => {
        setCurrentUser(newData);
        setIsStatusInfo({
          isOpen: true,
          successful:true,
          text: "Данные профиля обновлены!",
        });
      })
      .catch(error => {
        setIsStatusInfo({
          isOpen: true,
          successful:false,
          text: error,
        })
      })
      .finally(() => setIsLoading(false));
  }

  useEscapePress(onClickBurgerMenu, isBurgerMenuOpened);

  useEffect(() => {
    const path = location.pathname;
    const jwt = localStorage.getItem('jwt');
    if (jwt) {
      setIsLoading(true);
      auth
        .getUserInfo(currentUser.UserId)
        .then(data => {
          if (data) {
            setCurrentUser(data);
            setLoggedIn(true);
            history.push(path);
          }
        })
        .catch(error => {
          setIsStatusInfo({
            isOpen: true,
            successful:false,
            text: error,
          })
        })
        .finally(() => {
          setCompleteLoad(true);
          setIsLoading(false);
        })
    } else {
      setCompleteLoad(true);
    }
  }, [history]);

   // отслеживание изменений пользователя
   useEffect(() => {
    if (loggedIn) {
      setIsLoading(true);
      auth
        .getUserInfo(currentUser.UserId)
        .then(res => setCurrentUser(res))
        .catch(error => {
          setIsStatusInfo({
            isOpen: true,
            successful:false,
            text: error,
          })
        })
        .finally(() => setIsLoading(false));
    }
  }, [loggedIn]);

  return (
    <div className='page'>
      {!completeLoad ? (
        <Preloader isOpen={isLoading}/>
      ) : (
        <CurrentUserContext.Provider value={currentUser}>
          <Preloader isOpen={isLoading}/>
          <ErrorBoundary
            FallbackComponent={MyFallbackComponent}
            onError={(error, errorInfo) => console.log({ error, errorInfo })}
            onReset={() => {
              // Сброс состояния приложения
            }}
          >
            <StatusInfoBlock
            onClose={handleCloseStatusInfo}
            status={isStatusInfo}
          />
          </ErrorBoundary>

          <Route exact path = {headerEndpoints}>
            <Header
              onClickBurgerBtn={onClickBurgerMenu}
              isBurgerMenuOpened={isBurgerMenuOpened}
              authorized={loggedIn}
            />
          </Route>

          <Switch>
            <Route exact path="/" >
              <Main />
            </Route >

            <Route exact path="/signup">
              {!loggedIn ? (
                <Register
                  handleRegister={handleRegister}
                />
              ) : (
                <Redirect to='/' />
              )}
            </Route>

            <Route exact path="/signin">
            {!loggedIn ? (
              <Login handleLogin={handleLogin}/>
            ) : (
              <Redirect to='/' />
            )}
            </Route>

            <ProtectedRoute
              path="/educators"
              loggedIn={loggedIn}
              setIsLoading={setIsLoading}
              setIsStatusInfo={setIsStatusInfo}
              component={Educators}
            />

            <ProtectedRoute
              path='/profile'
              loggedIn={loggedIn}
              handleEditProfile={handleEditProfile}
              handleSignOut={handleSignOut}
              component={Profile}
            />


            <Route path="*">
              <NotFound goBack={returnOnPrevPage} />
            </Route>
          </Switch>
          <Route exact path={footerEndpoints}>
            <Footer />
          </Route>
        </CurrentUserContext.Provider>
      )}
    </div>
  );
}

export default App;
