import { Link, useLocation } from 'react-router-dom';
import Navigation from './Navigation';
import logo from '../images/logo.svg';

function Header({ onClickBurgerBtn, isBurgerMenuOpened, authorized}) {
  const location = useLocation();

  return (
    <header className={`header header_${location.pathname === '/' ? 'purple' : 'light'}`}>
      <Link to='/' className='header__logo-link'>
        <img className='header__logo' src={logo} alt='логотип страницы' />
      </Link>
      <Navigation authorization={authorized} onClickBurgerBtn={onClickBurgerBtn} isBurgerMenuOpened={isBurgerMenuOpened}/>
    </header>
  );
}

export default Header;
