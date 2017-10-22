import React from 'react';
import PropTypes from 'prop-types';
import TextInput from '../common/TextInput/TextInput';
import Button from '../common/Button/Button';

const ValidatorForm = ({onSubmit, onChange, tfnValue, errors, checking})=>{
  return (
    <form >
      <div className="form-group">
        <TextInput id="inputTfn" label="Tax File Number" value={tfnValue} onChange={onChange} name="inputTfn"
          placeHolder="8/9 digits TFN: 714 925 631"
          message={errors}
          valid={!(errors && errors.length > 0)}/>
      </div>
      <Button name={checking === true ? "Checking ..." : "Check"} onClick={onSubmit} disabled={checking}/>
    </form>
  );
};

ValidatorForm.propTypes = {
  tfnValue: PropTypes.string,
  errors: PropTypes.string,
  onSubmit: PropTypes.func.isRequired,
  onChange: PropTypes.func.isRequired
};

export default ValidatorForm;
