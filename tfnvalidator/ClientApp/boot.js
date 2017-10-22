import './css/site.css';

import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { AppContainer } from 'react-hot-loader';
import { BrowserRouter as Router } from 'react-router-dom';
import  Routes from './Routes';
import 'bootstrap';

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');

const renderApp = ()=>{
  ReactDOM.render(
    <AppContainer>
    <Router basename={baseUrl}>
      <Routes></Routes>
    </Router>
  </AppContainer>, document.getElementById('react-app'));
};
renderApp();
// Allow Hot Module Replacement
if (module.hot) {
    module.hot.accept('./Routes', () => {
        renderApp();
    });
}
