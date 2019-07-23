import React from 'react';
import { Route, Switch} from 'react-router-dom'
import NavbarMenu from './Components/NavBarMenu';
import About from './Components/About'
import Home from './Components/Home'
import Login from './Components/Login';
import Contact from './Components/Contact';
import NewUser from './Components/NewUser';
import PageNotFound from './Components/PageNotFound';



class App extends React.Component{
  
  constructor(props) {
    super(props);

    this.state = {
      isLogedIn: false
    };
  }
  
  checkIsLogedIn = ()=>{
    let token = window.localStorage.getItem("token");
    if(token !==null){
      this.setState({isLogedIn: true});
    }
    else{
      this.setState({isLogedIn: false});
    }
  }

  render() {
    return (
      <div className="App container">
        <NavbarMenu colore="blue" isLogedIn={this.state.isLogedIn}/>
        <Switch>
          <Route exact path="/" render={() => (<Home checkIsLogedInCB={this.checkIsLogedIn}/>)} />
          <Route exact path="/Home" render={() => (<Home checkIsLogedInCB={this.checkIsLogedIn}/>)} />/>
          <Route exact path="/About" component={About} />
          <Route exact path="/Login" render = {() => (<Login checkIsLogedInCB={this.checkIsLogedIn}/>)} />
          <Route exact path="/Contact" component={Contact} />
          <Route exact path="/Newuser" component={NewUser} />
          <Route exact path="/Logout" render={() => (<div>{window.localStorage.removeItem("token")}<Home checkIsLogedInCB={this.checkIsLogedIn}/></div>)} />/>
          <Route component={PageNotFound} />
        </Switch>
      </div>
    );
  }
}

export default App;
