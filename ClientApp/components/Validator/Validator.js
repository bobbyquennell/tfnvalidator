import React from 'react';
class Validator extends React.Component {

  constructor(){
    super();
    this.state = {
      tfn: '',
      result:''
    }
  }

  onSubmitHanlder =(event)=>{
    event.preventDefault();
    this.setState({result: this.state.tfn});
    return
  }
  onChangeHandler = (event) => {
    this.setState({tfn: event.target.value});
  };

  render(){
    return(
      <div className="col-sm-9">
        <h1>TFN Validator</h1>
        <form onSubmit={this.onSubmitHanlder} >
        <div className="form-group">
          <label htmlFor="inputTFN">Tax File Number</label>
          <input type="text" id="inputTFN" className="form-control" placeholder="8/9 digits TFN: 714 925 631" onChange={this.onChangeHandler}/>
        </div>
          <input type="submit" value="Check" className="btn btn-default"/>
        </form>
        <div>{this.state.result}</div>
      </div>

    );
  }
}

export default Validator;
