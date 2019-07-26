import React from 'react';
import Form from 'react-bootstrap/Form'
import Params from "../Global/Params"
import { Button } from "react-bootstrap";
import Alert from 'react-bootstrap/Alert'

export default class NewUser extends React.Component{
  
    constructor(props) {
        super(props);
        this.email = "";
        this.password = "";
        this.confirmPassword = "";
        this.username = "";

        this.state = {
          show: false,
          errorText: "",
          desibleBtn:false
        };
    }

    registerClick = (e) =>{
      e.preventDefault();   
      this.setState({desibleBtn:true});
      this.handleDismiss();  
      let status = 0;

      var data = {
        Email: this.email,
        Password: this.password,
        ConfirmPassword: this.confirmPassword,
        Username: this.username
      };

      fetch(Params.serverName + '/api/users/Register', {
        method: "POST",
        body: JSON.stringify(data),
        headers: {
          'Accept': 'application/json',
          "Content-Type": "application/json",
        }
      })
      .then((response => {
        status = response.status;  
        //console.log(`response = ${JSON.stringify(response)}`)          
        
        return response.json()
    }))
      .then(
        (result) => {
          console.log(`result = ${JSON.stringify(result)}`)
          console.log(`status = ${status}`)
          if(status===200) 
            this.props.history.push("/Login");
          else if(status===400) {
            this.setState({errorText: result.Message ,show:true});
          }
        },
        (error) => {
          console.log(`error = ${error}`)
          if(status===0){
            if(error == ("TypeError: Failed to fetch")){
              this.setState({errorText:"Service is not anvailable, please try later.",show:true});
            }
          }
          this.setState({desibleLoginBtn:false});
        }
      );  
      this.setState({desibleBtn:false});    
    }

    handleEmailChange = (event) =>{
      this.email = event.target.value;
      //console.log(this.email)
    }    
    handlePasswordChange = (event) =>{     
      this.password = event.target.value;
      //console.log(this.password)
    }
    handleConfirmPasswordChange = (event) =>{     
      this.confirmPassword = event.target.value;
      //console.log(this.confirmPassword)
    }

    handleUsernamChange = (event) =>{
      this.username = event.target.value;
    }

    handleDismiss = () =>{     
      this.setState( {show : false});
    }

    render() {
      return (        
        <div className='mt-5'>
          <Form  style={{maxWidth:'350px',margin:'0 auto'}} onSubmit={this.registerClick}>
              <Form.Group controlId="formBasicEmail">
                <Form.Label>Email address:</Form.Label>
                <Form.Control type="email" placeholder="Enter email" required onChange ={this.handleEmailChange}/>
                <Form.Text className="text-muted" >
                  We'll never share your email with anyone else.
                </Form.Text>
              </Form.Group>

              <Form.Group controlId="formBasicPassword">
                <Form.Label>Username:</Form.Label>
                <Form.Control type="text" placeholder="Username" required onChange={this.handleUsernamChange}/>
              </Form.Group>

              <Form.Group controlId="formBasicPassword">
                <Form.Label>Password:</Form.Label>
                <Form.Control type="password" placeholder="Password" required onChange ={this.handlePasswordChange}/>
              </Form.Group>

              <Form.Group controlId="formBasicConfirmPassword">
                <Form.Label>Renter Password:</Form.Label>
                <Form.Control type="password" placeholder="Password" required onChange ={this.handleConfirmPasswordChange}/>
              </Form.Group>
                          
              <input  type="submit" value="Register" disabled={this.state.desibleBtn} className="btn btn-primary" variant="primary" />
              <Button className='ml-4' variant="primary" onClick={()=>{this.props.history.push("/Login")}}>Login</Button>
              
              {(this.state.show) &&      
                <Alert className='mt-5' variant="danger" onClose={this.handleDismiss} dismissible>
                  <Alert.Heading>Error!</Alert.Heading>
                 
                    {this.state.errorText}
                  
                </Alert>
              }
              
          </Form> 
        </div>
        
      );
    }
  }