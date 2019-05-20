import React from 'react';
import {  Route, Switch  } from 'react-router-dom'
import NavbarMenu from './Components/NavBarMenu';
import About from './Components/About'
import Home from './Components/Home'
import Login from './Components/Login';
import Contact from './Components/Contact';


class App extends React.Component{
  
  
  render() {
    return (
      <div className="App container">
      <NavbarMenu colore="blue" />
       <Switch>
        <Route exact path="/" component={Home} />
        <Route exact path="/Home" component={Home} />
        <Route exact path="/About" component={About} />
        <Route exact path="/Login" component={Login} />
        <Route exact path="/Contact" component={Contact} />
      </Switch>
      </div>
    );
  }
}

export default App;
