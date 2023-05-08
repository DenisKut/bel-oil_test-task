function Footer() {
  const date = new Date();

  return(
    <footer className="footer">
      <h2 className="footer__title">
        Тестовый проект РУП Связьинформсервис Беларусьнефть.
      </h2>
      <div className="footer__navigation">
        <p className="footer__copyright">
          &copy;{date.getFullYear()}
        </p>
        <ul className="footer__links">
          <li>
            <a
              className="footer__link"
              href="https://github.com/DenisKut"
              target="_blank"
              rel="noreferrer"
            >
              Github
            </a>
          </li>
        </ul>
      </div>
    </footer>
  )
}

export default Footer;