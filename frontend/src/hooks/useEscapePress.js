import { useEffect } from "react";

function useEscapePress(callback, dependency) {
  useEffect(() => {
    if (dependency) {
      const onEscClose = event => {
        if (event.key === 'Escape') {
          callback();
        }
      }
      document.addEventListener('keyup', onEscClose);
      return () => {
        document.removeEventListener('keyup', onEscClose)
      };
    }
  }, [dependency])
}

export default useEscapePress;