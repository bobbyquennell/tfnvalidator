import * as React from 'react';
import { RouteComponentProps } from 'react-router';

export class Home extends React.Component {
    constructor(props){
      super(props);
    }
    render() {
        return (
          <div>
              <h1>Australian Tax File Number(TFN) Validator</h1>
              <h2>Technologies:</h2>
              <ul>
                  <li><strong>Backend:</strong>  ASP.NET Core, C#, WebAPI</li>
                  <li><strong>Frontend:</strong> React, ES6, Webpack, Babel</li>
                  <li><strong>Styling:</strong>Bootstrap </li>
              </ul>

          </div>
        );
    }
}
