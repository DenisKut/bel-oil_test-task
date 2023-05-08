import { useEffect, useState, useCallback} from 'react';

function useScreenWidth() {
  //Чтобы не вызывать лишний рендер компонентов, создаём мемо-функцию
  const getScreenWidth = useCallback(() => window.innerWidth, []);
  const [screenWidth, setScreenWidth] = useState(getScreenWidth());

  useEffect(() => {

    function handleScreenResize() {
      setScreenWidth(getScreenWidth());
    };

    window.addEventListener('resize', resizeController, false); // при монтировании ставим обработчик

    let resizeTimer;

    //Делаем resize 1 раз в секунду
    function resizeController() {
      if (!resizeTimer) {
        resizeTimer = setTimeout(() => {
          resizeTimer = null;
          handleScreenResize();
        }, 1000);
      }
    };

    // Убрать прослушивание события
    return () => window.removeEventListener('resize', handleScreenResize);
  }, [getScreenWidth]);

  return screenWidth;
}

export default useScreenWidth;