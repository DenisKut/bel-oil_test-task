import earthLogo from '../images/web_earth_logo.svg';

function Intro() {
  return(
    <section className='intro'>
      <div className='intro__about'>
        <h1 className='intro__title'>
          Тестовый проект Тарелко Дениса Игоревича.
        </h1>
        <a className='intro__link' href='https://github.com/DenisKut/bel-oil_test-task' target="_blank" rel="noreferrer">
          Узнать больше
        </a>
      </div>
      <img className='intro__logo' src={earthLogo} alt='Логотип WEB-шара' />
    </section>
  )
}

export default Intro;