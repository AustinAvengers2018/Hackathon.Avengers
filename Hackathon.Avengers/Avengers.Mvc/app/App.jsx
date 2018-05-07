//import React, { Component } from "react";
//import { Grid, Row, Col, ListGroup, ListGroupItem } from "react-bootstrap";
//import "bootstrap/dist/css/bootstrap.css";
//import "./App.css";
//import Header from './Components/Header';
//import AuditorResponseForm from './Components/AuditorResponseForm';
//import CaseDetails from './Components/CaseDetails';

const CASES = [
  { name: "Dirty Doc", pct: "84" },
  { name: "Opioid Addict", pct: "95" },
  { name: "Innocent Guy", pct: "20" },
  { name: "Dirty Doc 2", pct: "60" },
  { name: "Dirty Doc", pct: "84" },
  { name: "Opioid Addict", pct: "95" },
  { name: "Innocent Guy", pct: "20" },
  { name: "Dirty Doc 2", pct: "60" }
];

var App = React.createClass({ //extends Component {
    //constructor() {
    //    super();
    //    this.state = {
    //        activeCase: 0
    //    };
    //},
    getInitialState: function () {
        return ({ activeCase: 0 });
    },
    render() {
        const activeCase = this.state.activeCase;
        return (
            <div className="App">
                <Header />
                <div class="container">
                    <div class="row">
                        <div class="col-md-4 col-sm-6">
                            <ul class="list-group">
                                {CASES.map((place, index) => (
                                    <li class="list-group-item" header={place.name}
                                        key={index}
                                        onClick={() => {
                                            this.setState({ activeCase: index });
                                        }}
                                    >
                                        Details on Case
                                    </li>
                                ))}
                            </ul>
                        </div>
                        <div class="col-md-8 col-sm-8">
                            <CaseDetails key={activeCase} pct={CASES[activeCase].pct} name={CASES[activeCase].name} />
                            <hr />
                            <AuditorResponseForm />
                        </div>
                    </div>
                </div>
            </div>
        );
    }
});

//export default App;