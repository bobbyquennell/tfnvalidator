import * as React from 'react';
import { Route, BrowserRouter as Router } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import About from './components/About/About';
import Validator from './components/Validator/Validator';
import ErrorPage from './components/ErrorPage/ErrorPage';
// export const routes = <Layout>
//     <Route exact path='/' component={ Home } />
//     <Route path='/counter' component={ Counter } />
//     <Route path='/fetchdata' component={ FetchData } />
// </Layout>;

const Routes= ()=>{
  return (
    <Layout >
      <div>
        <Route exact path="/" component={Home} />
        <Route path="/counter" component={Counter} />
        <Route path="/fetchdata" component={FetchData} />
        <Route path="/about" component={About} />
        <Route path="/validator" component={Validator} />
        <Route path="/errorpage" component={ErrorPage} />
      </div>
    </Layout>
  );
};

export default Routes;
