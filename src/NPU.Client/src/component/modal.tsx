import React, { ReactNode } from 'react';

interface ModalProps {
  show: boolean;
  onClose: () => void;
  onOk?: () => void;
  title?: string;
  children: ReactNode;
  okText?: string;
}

const Modal: React.FC<ModalProps> = ({ show, onClose, onOk, title, children, okText }) => {
  if (!show) return null;

  return (
    <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50 z-50">
      <div className="bg-white rounded-lg shadow-lg w-11/12 md:w-1/2 lg:w-1/3 max-w-xs">
        <div className="border-b p-4 flex justify-between items-center">
          {title && <h2 className="text-xl font-semibold">{title}</h2>}
          <button onClick={onClose} className="text-xl font-semibold">&times;</button>
        </div>
        <div className="p-4">
          {children}
        </div>
        <div className="border-t p-4 flex justify-end">
          <button onClick={onClose} className="bg-slate-500 text-white px-4 py-2 rounded hover:bg-slate-600">
            Cancel
          </button>
          {onOk && <button onClick={onOk} className="ml-3 bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600">
            {okText ? okText : "Ok"}
          </button>}
        </div>
      </div>
    </div>
  );
};

export default Modal;
