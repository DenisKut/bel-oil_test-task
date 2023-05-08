function NotFound({ goBack }) {
  return(
    <main className="not-found">
      <div className="not-found__container">
        <span className="not-found__error-code">404</span>
        <span className="not-found__error">Страница не найдена</span>
        <button className="not-found__button" onClick={goBack}>
          Назад
        </button>
      </div>
    </main>
  )
}

export default NotFound;