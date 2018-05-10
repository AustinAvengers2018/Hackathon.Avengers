//import React, { Component } from 'react';

var CaseDetails = React.createClass({   //extends Component {
    render() {
        return (
            <div class= "container">
                <h1>{this.props.pct}% confident that {this.props.name} is commiting fraud</h1>
                <p>SSN: {this.props.ssn}</p>
                <p>Zip Code: {this.props.zip}</p>
                <p>Rx Number: {this.props.rx_num}</p>
                <p>NPI: {this.props.npi}</p>
            </div>
        );
    }
});

//export default CaseDetails;