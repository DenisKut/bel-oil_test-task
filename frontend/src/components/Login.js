import logo from '../images/logo.svg'
import useFormValidation from '../hooks/useFormValidation';
import { useEffect } from "react";
import { Link } from "react-router-dom";

function Login({handleLogin}) {
  const {
    values,
    handleChangeValues,
    resetForm,
    errors,
    isValid,
  } = useFormValidation();

  useEffect(() => {
    resetForm();
  }, [resetForm])

  function handleSubmit(event) {
    event.preventDefault();
    handleLogin(values)
  }

  return (
    <main className="login">
      <Link to='/' className="login__link">
        <img className='login__logo' alt='Логотип' src={logo}/>
      </Link>
      <h1 className='login__title'>
        Рады видеть!
      </h1>
      <form
        className="login__form"
        name="login"
        onSubmit={handleSubmit}
        autoComplete="off"
        noValidate
      >
        <label className='login__label'>
          <span className='login__label-text'>E-mail</span>
          <input
            className={`login__input ${errors.email && 'login__input_error'}`}
            name='email'
            onChange={handleChangeValues}
            value={values.email || ''}
            required
            // type='email'
            placeholder='pochta@mail.ru'
          />
          <span className='login__error'>
            {errors.email || ''}
          </span>
        </label>

        <label className='login__label'>
          <span className='login__label-text'>Пароль</span>
          <input
            className={`login__input ${errors.password && 'login__input_error'}`}
            name='password'
            onChange={handleChangeValues}
            value={values.password || ''}
            required
            minLength='8'
            maxLength='30'
            type='password'
            placeholder='Password'
          />
          <span className='login__error'>
            {errors.password || ''}
          </span>
        </label>
        <button
          className={
            `login__button ${
              !isValid && 'login__button_disabled'
            }`}
          type='submit'
          disabled={!isValid}
        >
          Войти
        </button>
        <span className='login__question'>
          Ещё не зарегистрированы?&nbsp;
          <Link to='/signup' className='login__signup-link'>
            Регистрация
          </Link>
        </span>
      </form>
    </main>
  )
}

export default Login;