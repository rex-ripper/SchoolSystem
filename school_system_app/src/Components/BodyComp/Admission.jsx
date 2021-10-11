import React, { Component } from 'react';
import { Form, Button, Table, Col, Row } from 'react-bootstrap';


export class Admission extends Component {
    constructor(props){
        super(props);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.state = {admitedStudentInfo : []};

    }

    async handleSubmit(event){
        event.preventDefault();
        await fetch("https://localhost:5001/Academic/Admission",{
            method : 'POST',
            mode : 'cors',
            headers:{
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                firstName : event.target.FirstName.value,
                lastName : event.target.LastName.value,
                address : event.target.Address.value,
                classOf : event.target.ClassOf.value,
                subject01 : event.target.Subject01.value,
                subject02 : event.target.Subject02.value,
                subject03 : event.target.Subject03.value
            })
        })
        .then(response => response.json())
        .then(items => { this.setState({ admitedStudentInfo: items }); })
        .catch(err => {
            throw new Error(err)
        });




    }
    render() {
        const { admitedStudentInfo } = this.state;
        return (

            <body className = "bg-light">
            <div>
            <Form onSubmit={this.handleSubmit}>

                    <Form.Group as={Row} className="mb-3" controlId="FirstName">
                        <Form.Label column sm={1}>
                        First Name
                        </Form.Label>
                        <Col sm={3}>
                            <Form.Control type="text" name = "FirstName" placeholder="First name (in Text)" required />
                        </Col>
                    </Form.Group>

                    <Form.Group as={Row} className="mb-3" controlId="LastName">
                        <Form.Label column sm={1}>
                        Last Name
                        </Form.Label>
                        <Col sm={3}>
                            <Form.Control type="text" name = "LastName" placeholder="Last name (in Text)" required />
                        </Col>
                    </Form.Group>
                    
                    <Form.Group as={Row} className="mb-3" controlId="Address">
                        <Form.Label column sm={1}>
                        Address
                        </Form.Label>
                        <Col sm={2}>
                            <Form.Control type="text" name= "Address" placeholder="Address (Optional Field)"  />
                        </Col>
                    </Form.Group>
                    
                    <Form.Group as={Row} className="mb-3" controlId="ClassOf">
                        <Form.Label column sm={1}>
                        Class
                        </Form.Label>
                        <Col sm={2}>
                            <Form.Control type="number" name = "ClassOf" placeholder="Clas name (in number)"  />
                        </Col>
                    </Form.Group>

                    <Form.Group as={Row} className="mb-3" controlId="Subject01">
                        <Form.Label column sm={1}>
                        Subject 01
                        </Form.Label>
                        <Col sm={2}>
                            <Form.Control type="text" name = "Subject01" placeholder="Subject name (in Text)" required />
                        </Col>
                    </Form.Group>
                    
                    <Form.Group as={Row} className="mb-3" controlId="Subject02">
                        <Form.Label column sm={1}>
                        Subject 02
                        </Form.Label>
                        <Col sm={2}>
                            <Form.Control type="text" name = "Subject02" placeholder="Subject name (in Text)" required />
                        </Col>
                    </Form.Group>

                    <Form.Group as={Row} className="mb-3" controlId="Subject03">
                        <Form.Label column sm={1}>
                        Subject 03
                        </Form.Label>
                        <Col sm={2}>
                            <Form.Control type="text" name = "Subject03" placeholder="Subject name (in Text)"  />
                        </Col>
                    </Form.Group>



                    <Button variant="primary" type="submit">
                        Submit
                    </Button>
                </Form>
                <Table className="mt-4" striped bordered hover size="sm">

                    <thead>
                        <tr>
                            <th>Id </th>
                            <th>Full Name</th>
                            <th>Admission Status</th>
                        </tr>
                    </thead>
                    <tbody>
                        <td>{admitedStudentInfo.Id}</td>
                        <td>{admitedStudentInfo.Name}</td>
                        <td>{admitedStudentInfo.IsAdmitted}</td>
                    </tbody>
                </Table>
            </div>
            </body>
        )
    }
}