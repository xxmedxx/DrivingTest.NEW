import React from 'react';
import Form from 'react-bootstrap/Form'
import Params from "../Global/Params"
import { Button } from "react-bootstrap";
import Alert from 'react-bootstrap/Alert'
import $ from 'jquery';
import { request } from 'https';

export default class NewUser extends React.Component{
  
    constructor(props) {
        super(props);
        this.email = "";
        this.password = "";
        this.confirmPassword = "";

        this.state = {
          show: false,
          errorText: ""
        };
    }

    registerClick = async () =>{    
      this.handleDismiss();  
      var data = {
        Email: this.email,
        Password: this.password,
        ConfirmPassword: this.confirmPassword,
        Authorization: 'TheReturnedToken'
      };
      $.ajax("")
      const heder = new Headers();
      heder.append("Content-Type", "application/json");
      const options ={
        method:"post",
        heder,
        body:JSON.stringify(data)
      };
      const req = new request(Params.serverName + '/api/Account/Register',options);
      const resp = await fetch(req);
      const status = await resp.status;
      console.log(">>> "+status);

      // //alert(data)
      // fetch(Params.serverName + '/api/Account/Register', {
      //   method: "post",
      //   headers: {
      //     "Content-Type": 'application/json; charset=utf-8',
      //   },
      //   body: JSON.stringify(data)
      // })
      //   .then(response => { 
      //                     response.text().then(text => {
      //                     //   const data = text && JSON.parse(text);
      //                     //   console.log("response>> " + JSON.stringify(data.Message));                            
      //                     //  this.setState({show : true, errorText : JSON.stringify(data.Message)
      //                     // + "\n" +JSON.stringify(data.ModelState["model.Email"][0])});
      //                     if ($.xhr.status == 400)
      //                       this.DisplayModelStateErrors($.xhr.responseJSON.ModelState);
      //                     });
      //                      //return response.json();
      //                     })
      //   .then(data => {console.log("data: " + data);})
      //   .catch(error => {
      //                     this.setState({show : true, errorText : JSON.stringify(error)})
      //                      console.log("error:www " + JSON.stringify(error));
      //                   });
      //   // {"Message":"The request is invalid.",
      //   //  "ModelState":{
      //   //   "model.Email":["The Email field is required."],
      //   //   "model.Password":["The Password must be at least 6 characters long.",
      //   //   "The Password field is required."]
      //   //  }
      //   // }
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

    handleDismiss = () =>{     
      this.setState( {show : false});
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

              <Form.Group controlId="formBasicConfirmPassword">
                <Form.Label>Renter Password:</Form.Label>
                <Form.Control type="password" placeholder="Password" required onChange ={this.handleConfirmPasswordChange}/>
              </Form.Group>
                          
              <Button className='' variant="primary" onClick={this.registerClick}>Register</Button>
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