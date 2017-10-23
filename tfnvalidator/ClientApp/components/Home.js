import * as React from 'react';
import { RouteComponentProps } from 'react-router';

export class Home extends React.Component {
  render() {
    return (
      <div>
        <h1>Australian Tax File Number(TFN) Validator</h1>
        <h2>Technologies:</h2>
        <ul>
          <li>
            <strong>Backend:</strong>
            ASP.NET Core, C#, WebAPI</li>
          <li>
            <strong>Frontend:</strong>
            React, ES6, Webpack, Babel</li>
          <li>
            <strong>Styling:</strong>Bootstrap, inline styling
          </li>
        </ul>
        <p>note: in order to display the loading.. effect, a 1s mock api delay included</p>

      </div>
    );
  }
}
