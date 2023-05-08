import useEscapePress from '../hooks/useEscapePress';

function StatusInfoBlock({ onClose, status: { isOpen, successful, text } }) {
  function handleClickOverlay(e) {
    e.stopPropagation();
  }

  useEscapePress(onClose, isOpen);

  return (
    <div
      className={`status-info-block ${isOpen && 'status-info-block_opened'}`}
      onClick={onClose}
    >
      <div className="status-info-block__container" onClick={handleClickOverlay}>
        <div
          className={`status-info-block__status ${
            successful
              ? 'status-info-block__status_success'
              : 'status-info-block__status_fail'
          }`}
        ></div>
        <p className="status-info-block__subtitle">{JSON.stringify(text)}</p>
        <button
          type="button"
          className="status-info-block__close-button"
          onClick={onClose}
        ></button>
      </div>
    </div>
  );
}

export default StatusInfoBlock;