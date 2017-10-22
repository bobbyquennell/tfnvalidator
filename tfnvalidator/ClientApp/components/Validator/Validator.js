
import React from 'react';
import ValidatorForm from './ValidatorForm';
import * as api from '../../api/validate';
class Validator extends React.Component {

  constructor(){
    super();
    this.state = {
      tfnToCheck: {
        value: '',
        linked: false,
        timeStamp: Date.now(),
        result: ''
      },
      errors: '',
      checking: false,
      result:''
    };
  }

  onSubmitHandler =(event)=>{
    event.preventDefault();
    this.setState({checking: true});
    let msg = '';
    api.verifyTfn(this.state.tfnToCheck).then(result => {
      //console.log(result);
      // this.setState({checking: false});
      msg = result.result;
      this.setState({result: msg, errors: '', checking: false});
    }).catch(error => {
      msg = error.message;
      this.setState({result: msg, errors: msg, checking: false});
      console.log(msg);
    });

    return;
  }
  onChangeHandler = (event) => {
    let tfnTocheck = {
      value: event.target.value,
      linked: false,
      timeStamp: Date.now(),
      result: ''
    };
    this.setState({tfnToCheck: tfnTocheck,result: '', errors: '', checking: false});
  };

  render(){
    return(
      <div className="col-sm-9">
        <h1>TFN Validator</h1>
        <ValidatorForm
          onSubmit={this.onSubmitHandler}
          onChange={this.onChangeHandler}
          tfnValue={this.state.tfnToCheck.value}
          errors={this.state.errors}
          checking={this.state.checking}
        />
        <div>{this.state.result}</div>
      </div>


    );
  }
}

export default Validator;
