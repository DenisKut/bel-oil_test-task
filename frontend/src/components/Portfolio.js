function Portfolio() {
  return(
    <section className="portfolio">
      <h2 className="portfolio__title">
        Портфолио
      </h2>
      <ul className="portfolio__projects">
        <li className="portfolio__project">
          <a
            className="portfolio__link"
            href="https://github.com/DenisKut/how-to-learn.git"
            target="_blank"
            rel="noreferrer"
          >
            Статичный сайт
          </a>
        </li>

        <li className="portfolio__project">
          <a
            className="portfolio__link"
            href="https://deniskut.github.io/russian-travel/index.html"
            target="_blank"
            rel="noreferrer"
          >
            Адаптивный сайт
          </a>
        </li>

        <li className="portfolio__project">
          <a
            className="portfolio__link"
            href="https://mesto.for.all.nomoredomains.rocks/"
            target="_blank"
            rel="noreferrer"
          >
            Одностраничное приложение
          </a>
        </li>
      </ul>
    </section>
  )
}

export default Portfolio;