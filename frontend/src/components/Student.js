import photo from '../images/profilePhoto.jpg'

function Student() {
  const date = new Date();

  return(
    <section className="student">
      <h2 className="student__heading">
        Студент
      </h2>
      <div className="student__biography-container">
        <div className="student__biography">
          <h3 className="student__name">
            Денис
          </h3>
          <p className="student__subtitle">
            Фуллстэк-разработчик, {date.getFullYear() - 2002} год
          </p>
          <p className="student__text">
            Я родился в Бобруйске, сейчас живу в Гомеле, учусь на инженера-системного программиста
            в ГГТУ имени П.О.Сухого, 4-й курс. С раннего возраста занимаюсь спортом: борьбой, гиревым спортом,
            пауерлифтингом. Очень люблю музыку, особенно рок! Сам играю на гитаре и стараюсь постоянно развиваться.
            Прошёл курс веб-разработчика в Яндекс-практикуме.
          </p>
          <ul className="student__peronal-links">
            <li>
              <a
                className="student__personal-link"
                href="https://vk.com/tarelko_den"
                target="_blank"
                rel="noreferrer"
              >
                ВКонтакте
              </a>
            </li>
            <li>
              <a
                className="student__personal-link"
                href="https://github.com/DenisKut"
                target="_blank"
                rel="noreferrer"
              >
                Github
              </a>
            </li>
            <li>
              <a
                className="student__personal-link"
                href="https://www.linkedin.com/in/dzianis-tarelka-370258251"
                target="_blank"
                rel="noreferrer"
              >
                LinkedIn
              </a>
            </li>
          </ul>
        </div>
        <img
          className="student__photo"
          src={photo}
          alt="фотография разработчика"
        />
      </div>

    </section>
  )
}

export default Student;