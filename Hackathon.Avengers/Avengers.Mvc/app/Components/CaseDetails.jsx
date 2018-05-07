//import React, { Component } from 'react';

var CaseDetails = React.createClass({   //extends Component {
    render() {
        return (
            <h1>{this.props.pct}% confident that {this.props.name} is commiting fraud</h1>
        );
    }
});

//export default CaseDetails;