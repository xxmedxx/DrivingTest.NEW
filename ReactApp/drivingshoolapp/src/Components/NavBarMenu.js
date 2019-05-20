import React from 'react';
import {
  Collapse,
  Navbar,
  NavbarToggler,
  NavbarBrand,
  Nav,
  NavItem,
  NavLink,
  UncontrolledDropdown,
  DropdownToggle,
  DropdownMenu,
  DropdownItem } from 'reactstrap';

export default class NavbarMenu extends React.Component {
  constructor(props) {
    super(props);

    this.toggle = this.toggle.bind(this);
    this.state = {
      isOpen: false
    };
  }
  toggle() {
    this.setState({
      isOpen: !this.state.isOpen
    });
  }
  
  render() {
    return (
      <div>
        <Navbar color="info" light expand="md">
          <NavbarBrand href="/">Driving School</NavbarBrand>
          <NavbarToggler onClick={this.toggle} />
          <Collapse isOpen={this.state.isOpen} navbar>
            <Nav className="ml-auto bg-blue" navbar variant="pills" >
              <NavItem active>
                <NavLink href="#/Home">Home</NavLink>
              </NavItem>
              <UncontrolledDropdown nav inNavbar>
                  <DropdownToggle nav caret>
                    Exams
                  </DropdownToggle>
                  <DropdownMenu right>
                    <DropdownItem>
                      <NavLink href="/components/About">My Exams</NavLink>
                    </DropdownItem>
                    <DropdownItem>
                      <NavLink href="/components/About">New Exam</NavLink>
                    </DropdownItem>
                  </DropdownMenu>
              </UncontrolledDropdown>
              <UncontrolledDropdown nav inNavbar>
                  <DropdownToggle nav caret>
                    Sereis
                  </DropdownToggle>
                  <DropdownMenu right>
                    <DropdownItem>
                      <NavLink href="/components/About">All Sereis</NavLink>
                    </DropdownItem>
                    <DropdownItem>
                      <NavLink href="/components/About">New Sereis</NavLink>
                    </DropdownItem>
                  </DropdownMenu>
              </UncontrolledDropdown>
              <NavItem >
                <NavLink href="#/Contact">Contact us</NavLink>
              </NavItem>
              <NavItem >
                <NavLink href="#/About">About</NavLink>
              </NavItem>
              <NavItem >
                <NavLink href="#/Login">Log in/Log out</NavLink>
              </NavItem>             
            </Nav>
            
          </Collapse>
        </Navbar>
      </div>
    );
  }
}
