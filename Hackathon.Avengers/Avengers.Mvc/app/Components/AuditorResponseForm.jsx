//import React, { Component } from 'react';
//import { ButtonGroup, ListGroup, ListGroupItem, Radio, FormGroup, ControlLabel, FormControl, Button } from "react-bootstrap";

var AuditorResponseForm = React.createClass({   //extends Component {
    render() {
        return (
            <form>
                <div class="form-group">
                    <label for="exampleFormControlTextarea1">Comments</label>
                    <textarea class="form-control" id="exampleFormControlTextarea1" rows="3"></textarea>
                </div>
                <div class="form-group">
                    <label>Conclusion</label>
                    <div class="custom-control custom-radio">
                        <input type="radio" id="customRadio1" name="customRadio" class="custom-control-input"/>
                        <label class="custom-control-label" for="customRadio1">Further Investigation Required.</label>
                    </div>
                    <div class="custom-control custom-radio">
                        <input type="radio" id="customRadio2" name="customRadio" class="custom-control-input"/>
                        <label class="custom-control-label" for="customRadio2">No Fraud.</label>
                    </div>
                </div>
                <button type="submit" class="btn btn-primary">Mark case as reviewed</button>
            </form>
        );
    }
});

//export default AuditorResponseForm;