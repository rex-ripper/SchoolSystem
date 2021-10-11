import React, { Component } from 'react'
import { Form, Button, Col, Row, Table } from 'react-bootstrap';


export class Class extends Component {
    constructor(props) {
        super(props);
        this.handleSubmit=this.handleSubmit.bind(this);
        this.state = { classInfo: [] }
    }

    async handleSubmit(event){
        event.preventDefault();
        await fetch('https://localhost:5001/Academic/Class', {
            method: "POST",
            mode: 'cors',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ ClassOf: event.target.ClassOf.value })
        })
            .then(response => response.json())
            .then(items => this.setState({ classInfo: items }))
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
                        <Form.Group as={Row} className="mb-3" controlId="ClassOf">
                            <Form.Label column sm={1}>
                                Class Name
                            </Form.Label>
                            <Col sm={2}>
                                <Form.Control type="number" placeholder="Clas name (in number)" required />
                            </Col>
                        </Form.Group>
                        <Button variant="primary" type="submit">
                            Submit
                        </Button>
                    </Form>

                    <Table className="mt-4" striped bordered hover size="sm">

                        <thead>
                            <tr>
                                <th>Class </th>
                                <th>English Teacher</th>
                                <th>Math Teacher</th>
                                <th>Physics Teacher</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>{classInfo.ClassOf}</td>
                                <td>{classInfo.EnglishTeacher}</td>
                                <td>{classInfo.MathTeacher}</td>
                                <td>{classInfo.EnglishTeacher}</td>

                            </tr>
                        </tbody>
                    </Table>
                </div>
            </body>
        )
    }
}