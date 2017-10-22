
import React from 'react';
import PropTypes from 'prop-types';

const Button = ({name, onClick, disabled })=>{
  return(
    <button className="btn btn-default" onClick={onClick} disabled={disabled}>{name}</button>
  );
};

Button.propTypes = {
  name: PropTypes.string.isRequired,
  onClick: PropTypes.func.isRequired,
  disabled: PropTypes.bool.isRequired
};
export default Button;
