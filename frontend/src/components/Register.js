import logo from '../images/logo.svg'
import useFormValidation from '../hooks/useFormValidation';
import { useEffect } from "react";
import { Link } from "react-router-dom";

function Register({ handleRegister }) {
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
    handleRegister(values);
  }

  return (
    <main className="register">
      <Link to='/' className="register__link">
        <img className='register__logo' alt='Логотип' src={logo}/>
      </Link>
      <h1 className='register__title'>
        Добро пожаловать!
      </h1>
      <form
        className="register__form"
        name="register"
        onSubmit={handleSubmit}
        autoComplete="off"
        noValidate
      >
        <label className='register__label'>
          <span className='register__label-text'>Имя</span>
          <input
            className={`register__input ${errors.name && 'register__input_error'}`}
            name='name'
            onChange={handleChangeValues}
            value={values.name || ''}
            required
            minLength='2'
            maxLength='30'
            type='text'
            placeholder='Денис'
          />
          <span className='register__error'>
            {errors.name || ''}
          </span>
        </label>

        <label className='register__label'>
          <span className='register__label-text'>E-mail</span>
          <input
            className={`register__input ${errors.email && 'register__input_error'}`}
            name='email'
            onChange={handleChangeValues}
            value={values.email || ''}
            required
            type='email'
            placeholder='pochta@mail.ru'
          />
          <span className='register__error'>
            {errors.email || ''}
          </span>
        </label>

        <label className='register__label'>
          <span className='register__label-text'>Пароль</span>
          <input
            className={`register__input ${errors.password && 'register__input_error'}`}
            name='password'
            onChange={handleChangeValues}
            value={values.password || ''}
            required
            minLength='8'
            maxLength='30'
            type='password'
            placeholder='Password'
          />
          <span className='register__error'>
            {errors.password || ''}
          </span>
        </label>
        <button
          className={
            `register__button ${
              !isValid && 'register__button_disabled'
            }`}
          type='submit'
          disabled={!isValid}
        >
          Зарегистрироваться
        </button>
        <span className='register__question'>
          Уже зарегистрированы?&nbsp;
          <Link to='signin' className='register__signin-link'>
            Войти
          </Link>
        </span>
      </form>
    </main>
  )
}

export default Register;