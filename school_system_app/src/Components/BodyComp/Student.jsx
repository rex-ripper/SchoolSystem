import React, { Component } from 'react';
import { Form, Button, Col, Row, Table } from 'react-bootstrap';


export class Student extends Component {
    constructor(props) {
        super(props);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.state = { classInfo: [] };
    }

    async handleSubmit(event) {
        event.preventDefault();
        await fetch('https://localhost:5001/Academic/Student', {
            method: 'POST',
            mode: 'cors',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                FirstName: event.target.FirstName.value,
                LastName: event.target.LastName.value
            })
        })
            .then(response => response.json())
            .then(items => { this.setState({ classInfo: items }); })
            .catch(err => {
                throw new Error(err)
            });

    }

    render() {
        const { classInfo } = this.state;

        return (
            <body className="bg-light" >
                <div>

                    <Form onSubmit={this.handleSubmit}>
                        <Form.Group as={Row} className="mb-3" controlId="FirstName">
                            <Form.Label column sm={1}>
                                First Name
                            </Form.Label>
                            <Col sm={2}>
                                <Form.Control type="text" name="FirstName" placeholder="In Text" required />
                            </Col>
                        </Form.Group>
                        <Form.Group as={Row} className="mb-3" controlId="LastName">
                            <Form.Label column sm={1}>
                                Last Name
                            </Form.Label>
                            <Col sm={2}>
                                <Form.Control type="text" name="LastName" placeholder="In Text" required />
                            </Col>
                        </Form.Group>
                        <Button variant="primary" type="submit">
                            Submit
                        </Button>
                    </Form>

                    <Table className="mt-4" striped bordered hover size="sm">

                        <thead>
                            <tr>
                                <th>Name </th>
                                <th>Class </th>
                                <th>Subjects</th>
                                <th>English Teacher</th>
                                <th>Math Teacher</th>
                                <th>Physics Teacher</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>{classInfo.Name}</td>
                                <td>{classInfo.ClassOf}</td>
                                <td>{classInfo.Subjects}</td>
                                <td>{classInfo.EnglishTeacher}</td>
                                <td>{classInfo.MathTeacher}</td>
                                <td>{classInfo.PhysicsTeacher}</td>

                            </tr>
                        </tbody>
                    </Table>
                </div>
            </body>
        )
    }
}
export default Student
