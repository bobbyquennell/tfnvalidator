import React from 'react';
import PropTypes from 'prop-types';
import styles from './TextInput-styles';
const TextInput = function({ id, label, value, name, placeHolder, onChange, message="", valid= true}){
  return (
    <div>
      <label htmlFor={name}>{label}</label>
      <input className="form-control" type="text" value={value} name={name}
        placeholder={placeHolder} onChange={onChange} id={id}/>
      <div style={styles.error}>
        <span>{((!valid)&& message !== null) || undefined ? `invalid: ${message}` : null }</span>
      </div>
    </div>

  );
};

TextInput.propTypes = {
  id: PropTypes.string.isRequired,
  label: PropTypes.string.isRequired,
  value: PropTypes.string.isRequired,
  onChange: PropTypes.func.isRequired,
  name: PropTypes.string.isRequired,
  placeHolder: PropTypes.string,
  message: PropTypes.string,
  valid: PropTypes.bool
};
export default TextInput;
