import * as React from "react";
import { Link, NavLink } from "react-router-dom";

export class NavMenu extends React.Component{

  render() {
    return (
      <div className="main-nav">
        <div className="navbar navbar-inverse bg-primary">
          <div className="navbar-header">
            <button type="button" className="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
              <span className="sr-only">Toggle navigation</span>
              <span className="icon-bar"/>
              <span className="icon-bar"/>
              <span className="icon-bar"/>
            </button>
            <Link className="navbar-brand" to={"/"}>TFN Validator</Link>
          </div>
          <div className="clearfix"/>
          <div className="navbar-collapse collapse">
            <ul className="nav navbar-nav">
              <li>
                <NavLink to={"/"} exact activeClassName="active">
                  <span className="glyphicon glyphicon-home"/>
                  Home
                </NavLink>
              </li>
              {/* <li>
                                <NavLink to={ "/counter" } activeClassName="active">
                                    <span className="glyphicon glyphicon-education"></span> Counter
                                </NavLink>
                            </li>
                            <li>
                                <NavLink to={ "/fetchdata" } activeClassName="active">
                                    <span className="glyphicon glyphicon-th-list"></span> Fetch data
                                </NavLink>
                            </li> */}
              <li>
                <NavLink to={"/validator"}>
                  <span className="glyphicon glyphicon-th-list"/>Validator
                </NavLink>
              </li>
              <li>
                <NavLink to={"/about"}>
                  <span className="glyphicon glyphicon-th-list"/>About
                </NavLink>
              </li>

            </ul>
          </div>
        </div>
      </div>
    );
  }
}
