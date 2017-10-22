// This file mocks a web API by working with the hard-coded data below.
// It uses setTimeout to simulate the delay of an AJAX call.
// All calls return promises.
import axios from 'axios';
/* mock data */
let TFNs = [
  {
    value: '123456789',
    linked: false,
    date: '2017-08-01',
    timeStamp: '',
    result: ''
  }, {
    value: '123459876',
    linked: false,
    timeStamp: '',
    result: ''
  }
];
/* simulate api delay */
const delay = 2000;

export  function mock_verifyTfnWithSuccess(tfn){
  return  new Promise(
    (resolve, reject) =>{
      setTimeout(() => {
        //mock up server side validation:
        const reg = new RegExp("^[0-9]{1,10}$");
        let isValidNumber = reg.test(tfn.value);
        if(!isValidNumber){
          reject(new Error('only numbers are accepted'));
        }
        if(tfn.value.length < 8 || tfn.value.length > 9){
          reject(new Error('TFN should be 8 or 9 digits'));
        }
        //mock result from server-side validation
        tfn.result = "valid TFN";
        resolve(tfn);
      }, delay);
    }
  );
}

export  function verifyTfn(tfn){
  return  new Promise(
    (resolve, reject) =>{

      const reg = new RegExp("^[0-9]{1,10}$");
      let num = tfn.value.replace(/\s+/g, '');
      let isValidNumber = reg.test(num);
      if(!isValidNumber){
        //reject(new Error("linked"));
        reject(new Error('only numbers are accepted'));
        return;
      }
      if(num.length < 8 || num.length > 9){
        reject(new Error('TFN should be 8 or 9 digits'));
        return;
      }
      setTimeout(() => {
        axios.get(`/api/validate?tfn=${num}`)
          .then(response=>{
            tfn.result = response.data;
            resolve(tfn);
          })
          .catch(error=>{
            //reject(new Error("linked"));
            reject(new Error(error.response.data));
          });
      }, delay);
    }
  );
}
