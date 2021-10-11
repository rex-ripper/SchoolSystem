import React, { Component } from 'react';
import { Table } from 'react-bootstrap';


export class Toppers extends Component {


    constructor(props) {
        super(props);
        this.state = { toppers: [] }
    }

    getStudents() {
        fetch('https://localhost:5001/Academic/Toppers')
            .then(response => response.json())
            .then(items => { this.setState({ toppers: items }); });
    }

    componentDidMount() {
        this.getStudents();
    }

    componentDidUpdate() {
        this.getStudents();
    }


    render() {
        const {toppers} = this.state;

        return (
            <div>
                <Table className="mt-4" striped bordered hover size="sm">

                    <thead>
                        <tr>
                            <th>Class Name</th>
                            <th>First Positon</th>
                            <th>Second Positon</th>
                            <th>Third Positon</th>

                        </tr>
                    </thead>
                    <tbody>
                        {toppers.map(toppers => (
                            <tr key = {toppers.Id}>

                                <td>{toppers.ClassOf}</td>
                                <td>{toppers.First}</td>
                                <td>{toppers.Second}</td>
                                <td>{toppers.Third}</td>

                            </tr>
                        ))}
                    </tbody>
                </Table>
            </div>
        )
    }
}