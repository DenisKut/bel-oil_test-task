function Preloader ({ isOpen }) {
  return(
    <>
      {isOpen && (
        <div className="preloader">
          <div className="preloader-hourglass"></div>
        </div>
      )}
    </>
  );
}

export default Preloader;