//import React, { Component } from 'react';

var CaseDetails = React.createClass({   //extends Component {
    render() {
        return (
            <div class= "container">
                <h1>{this.props.pct}% confident that {this.props.name} is commiting fraud</h1>
                <h3>Fraud committed by prescriber/provider</h3>
                <div class="row">
                    <div class="col-md-6">
                        <p>Name: Last, First</p>
                        <p>SSN: {this.props.ssn}</p>
                        <p>Zip Code: {this.props.zip}</p>
                        <p>NPI: {this.props.npi}</p>
                    </div>
                    <div class="col-md-6">
                        <p>Drug: druge_name here</p>
                        <p>Rx Number: {this.props.rx_num}</p>
                        <p>Days Supply: num here</p>
                        <p>Rx Refill Date: xx/xx/xxxx</p>
                    </div>
                </div>
            </div>
        );
    }
});

//export default CaseDetails;