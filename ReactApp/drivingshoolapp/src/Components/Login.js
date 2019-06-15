import React from 'react';
import Form from 'react-bootstrap/Form'
import { Button } from "react-bootstrap";

export default class Login extends React.Component{
  
  constructor(props) {
    super(props);
      this.email = "";
      this.password = "";
  }
    loginClick = () =>{
      alert(this.email)
      fetch('http://http://localhost:1669//api/login', {
        method: "post",
        headers: {
          "Content-Type": "application/x-www-form-urlencoded"
        },
        //make sure to serialize your JSON body
        body: `Email=${this.email}&Password=${this.password}`
      })
        .then(response => response.json())
        .then(data => {this.checkAndRedirect(data);})
        .catch(error => alert('Error:', error));
    }

    checkAndRedirect(user){
      console.log(user);
      if(user!== null)
        {
          this.props.history.push("/home");
        }
      alert(user);
    }

    handleEmailChange = (event) =>{
      this.email = event.target.value;
      console.log(this.email)
    }
    handlePasswordChange = (event) =>{     
      this.password = event.target.value;
      console.log(this.password)
    }

    render() {
      return (
        <div className='mt-5'>
          <Form  style={{maxWidth:'350px',margin:'0 auto'}}>
              <Form.Group controlId="formBasicEmail">
                <Form.Label>Email address:</Form.Label>
                <Form.Control type="email" placeholder="Enter email" onChange ={this.handleEmailChange}/>
                <Form.Text className="text-muted" type="email" required>
                  We'll never share your email with anyone else.
                </Form.Text>
              </Form.Group>

              <Form.Group controlId="formBasicPassword">
                <Form.Label>Password:</Form.Label>
                <Form.Control type="password" placeholder="Password" required onChange ={this.handlePasswordChange}/>
              </Form.Group>

              <Form.Group controlId="formBasicChecbox">
                <Form.Check type="checkbox" label="Remember me." />
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