//const CASES = [
//    { name: "Dirty Doc", pct: "84" },
//    { name: "Opioid Addict", pct: "95" },
//    { name: "Innocent Guy", pct: "20" },
//    { name: "Dirty Doc 2", pct: "60" },
//    { name: "Dirty Doc", pct: "84" },
//    { name: "Opioid Addict", pct: "95" },
//    { name: "Innocent Guy", pct: "20" },
//    { name: "Dirty Doc 2", pct: "60" }
//];


//TODO: Styles are not being applied

var App = React.createClass({ //extends Component {
    render: function() {
        return (
            <div class="navbar navbar-inverse">
                <div class="container">
                    <div class="navbar-header">
                        Opioid Dashboard - Auditor View
                    </div>
                    <p class="navbar-text navbar-right">Signed in as <a href="#" class="navbar-link">Mark Otto</a></p>
                </div>
            </div>
        );
    }
});

ReactDOM.render(<App />, document.getElementById('content'));
