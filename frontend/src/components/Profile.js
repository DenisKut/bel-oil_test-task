import { useEffect, useContext } from "react";
import useFormValidation from '../hooks/useFormValidation';
import { CurrentUserContext } from "../contexts/CurrentUserContext";

function Profile({ handleEditProfile, handleSignOut }) {
  const {
    values,
    handleChangeValues,
    resetForm,
    errors,
    isValid,
  } = useFormValidation();

  const currentUser = useContext(CurrentUserContext);
  const validity = (!isValid || (currentUser.name === values.name && currentUser.email === values.email));

  function handleSubmit(event) {
    event.preventDefault();
    handleEditProfile(values)
  }

  //загружаем данные из апи в компоненты
  useEffect(() => {
    if(currentUser)
      resetForm(currentUser, {}, true);
  }, [currentUser, resetForm]);

  return(
    <main className="profile">
      <h1 className='profile__title'>
        {`Привет, ${currentUser.name || ''}!`}
      </h1>
      <form
        className="profile__form"
        name="profile"
        onSubmit={handleSubmit}
        autoComplete="off"
        noValidate
      >
        <label className='profile__label'>
          <span className='profile__label-text' id="name">Имя</span>
          <input
            className={`profile__input ${errors.name && 'profile__input_error'}`}
            name='name'
            onChange={handleChangeValues}
            value={values.name || ''}
            required
            minLength='2'
            maxLength='30'
            type='text'
          />
          <span className='profile__error'>
            {errors.name || ''}
          </span>
        </label>

        <label className='profile__label'>
          <span className='profile__label-text' id="email">E-mail</span>
          <input
            className={`profile__input ${errors.email && 'profile__input_error'}`}
            name='email'
            onChange={handleChangeValues}
            value={values.email || ''}
            required
            type='email'
          />
          <span className='profile__email-error'>
            {errors.email || ''}
          </span>
        </label>
        <button
          className={
            `profile__button-edit ${
              validity && 'profile__button-edit_disabled'
            }`}
          type='submit'
          disabled={validity ? true : false}
        >
          Редактировать
        </button>
        <button
          type="submit"
          className="profile__button-exit"
          onClick={handleSignOut}
        >
          Выйти из аккаунта
        </button>
      </form>
    </main>
  )
}

export default Profile;