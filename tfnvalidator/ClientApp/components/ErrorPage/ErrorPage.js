import React from 'react';

const ErrorPage = ()=>{
  return (
    <div>
      <h2>Woops:</h2>
      <p>This tool doesn’t allow multiple attempts for similar TFNs within 30 seconds</p>
      <p>Try again later</p>
    </div>
  );
};

export default ErrorPage;
