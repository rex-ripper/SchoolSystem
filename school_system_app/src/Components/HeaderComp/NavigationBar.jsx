import React, { Component } from 'react';
import { Nav, Navbar, NavLink} from 'react-bootstrap';



export class NavigationBar extends Component {
    render() {
        return (
            <Navbar bg = "light" expand = "lg">
                <Navbar.Toggle aria-controls = "basic-navbar-nav" />
                <Navbar.Collapse id = "basic-navbar-nav">
                    <Nav>
                        <NavLink className = "d-inline p-2 bg-light test-white" href = "/"> Home </NavLink>
                        <NavLink className = "d-inline p-2 bg-light test-white" href = "/Admission"> Admission </NavLink>
                        <NavLink className = "d-inline p-2 bg-light test-white" href = "/Students"> Students </NavLink>
                        <NavLink className = "d-inline p-2 bg-light test-white" href = "/Student"> Student Info </NavLink>
                        <NavLink className = "d-inline p-2 bg-light test-white" href = "/Class"> Class Info </NavLink>
                        <NavLink className = "d-inline p-2 bg-light test-white" href = "/Toppers"> Toppers </NavLink>
                    </Nav>
                </Navbar.Collapse>
            </Navbar>
        )
    }
}