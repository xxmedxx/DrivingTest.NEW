import React from 'react';
import Form from 'react-bootstrap/Form'
import { Button,Alert } from "react-bootstrap";
import Params from "../Global/Params"

export default class Login extends React.Component{
  
  constructor(props) {
    super(props);
      this.email = "";
      this.password = "";
      this.username="";
      this.state = {
        desibleLoginBtn:false,
        loginError:false,
        errorText:"",
      }
  }

  loginClick = (e) =>{
    e.preventDefault();
    this.setState({desibleLoginBtn:true});
    let status=0;
    let error;
    
    fetch(Params.serverName + `api/users/login?Email=${this.email}&Password=${this.password}&Username=${this.username}`, {
        method: "get",
        headers: {
          "Content-Type": "application/x-www-form-urlencoded",
        }
      })
      .then((response => {
        status = response.status;            
        if(status===404){
          this.setState({errorText:"Password or Email address is invalid, please try again!",loginError:true});
        }
        return response.json()
    }))
      .then(
        (result) => {
          console.log(`result = ${JSON.stringify(result)}`)
          this.checkAndRedirect(result);
        },
        (error) => {
          if(status===0){
            if(error == ("TypeError: Failed to fetch")){
              this.setState({errorText:"Service is not anvailable, please try later.",loginError:true});
            }
          }
          this.setState({desibleLoginBtn:false});
        }
      );
  }

  checkAndRedirect(token){
    if(token!== null)
      {
        window.localStorage.setItem("token",token);
        this.props.checkIsLogedInCB();
        window.location = "#/home"
      }
  }

  handleEmailChange = (event) =>{
    this.email = event.target.value;
  }
  
  handlePasswordChange = (event) =>{     
    this.password = event.target.value;
  }
  
  render() {
    return (
      <div className='mt-5'>
        <Form  style={{maxWidth:'420px',margin:'0 auto'}} onSubmit={this.loginClick}>
            <Form.Group controlId="formBasicEmail">
              <Form.Label>Email address:</Form.Label>
              <Form.Control type="email" placeholder="Enter email" required onChange={this.handleEmailChange}/>
              <Form.Text className="text-muted">
                We'll never share your email with anyone else.
              </Form.Text>
            </Form.Group>
            
            <Form.Group controlId="formBasicPassword">
              <Form.Label>Password:</Form.Label>
              <Form.Control type="password" placeholder="Password" required onChange={this.handlePasswordChange}/>
            </Form.Group>

            <Form.Group controlId="formBasicChecbox">
              <Form.Check type="checkbox" label="Remember me." />
            </Form.Group>

            <input  type="submit" value="Login" disabled={this.state.desibleLoginBtn} className="btn btn-primary" variant="primary" />
              
            <Button className='ml-4' variant="primary" onClick={()=>{window.location = "#/Newuser"}}>Register</Button>
            
            {
              this.state.loginError && 
              <Alert variant="danger" className="mt-4"> 
                {this.state.errorText}
              </Alert>
            }
        </Form> 
      </div>
      
    );
  }
}