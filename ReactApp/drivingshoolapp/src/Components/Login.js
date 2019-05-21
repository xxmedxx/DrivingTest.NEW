import React from 'react';
import Form from 'react-bootstrap/Form'
import { Button } from "react-bootstrap";

export default class Login extends React.Component{
  
    loginClick = () =>{
      alert("in click")
      fetch('http://localhost:2275//api/login', {
        method: "post",
        headers: {
          "Content-Type": "application/x-www-form-urlencoded"
        },
        //make sure to serialize your JSON body
        body: "Email=h_mohamed@live.net&Password=12345"
      })
        .then(response => response.json())
        .then(data => {this.checkAndRedirect(data);})
        .catch(error => alert('Error:', error));
    }

    checkAndRedirect(user){
      console.log(user);
      if(user!== null)
        this.props.history.push("/home")
      alert(user);
    }
  
    render() {
      return (
        <div className='mt-5'>
          <Form  style={{maxWidth:'350px',margin:'0 auto'}}>
              <Form.Group controlId="formBasicEmail">
                <Form.Label>Email address:</Form.Label>
                <Form.Control type="email" placeholder="Enter email" />
                <Form.Text className="text-muted" type="email" required>
                  We'll never share your email with anyone else.
                </Form.Text>
              </Form.Group>

              <Form.Group controlId="formBasicPassword">
                <Form.Label>Password:</Form.Label>
                <Form.Control type="password" placeholder="Password" required/>
              </Form.Group>

              <Form.Group controlId="formBasicChecbox">
                <Form.Check type="checkbox" label="Check me out" />
              </Form.Group>

              <Button variant="primary" onClick={this.loginClick}>
                Login
              </Button>
              
              <Button className='ml-4' variant="primary" onClick={this.loginClick}>
                Register
              </Button>
          </Form> 
        </div>
        
      );
    }
  }