import React, { Component } from 'react';
import './App.css';
import axios from 'axios'

class App extends Component {
  constructor () {
    super() 
    this.state = { 
      domain: '',
      subDomains: [],
      subDomainItems: ''
    }
    this.handleSubDomainClick = this.handleSubDomainClick.bind(this)
    this.handleIPClick = this.handleIPClick.bind(this)
    this.handleDomainChange = this.handleDomainChange.bind(this)
    this.bindSubDomainData.bind(this)
  }

  handleDomainChange(event) {
    this.setState({domain: event.target.value})
    this.setState({subDomains: []})
    this.setState({subDomainItems: ''})
  }
  
bindSubDomainData(data) {
  if (data !== undefined)
  {
    this.setState({subDomains: data});
    this.setState({subDomainItems: data.map((item, index) =>
      <tr><td>{index}</td><td>{item}</td><td></td></tr>
      )});
    }
  }

  handleSubDomainClick () {
    if (this.state.domain.length > 0)
    {
      axios.get('http://localhost:50948/subdomain/enumerate/' + this.state.domain)
      .then(response => { this.bindSubDomainData(response.data) })
      .catch(err => console.error("error: " + err)); 
    }
  }

  handleIPClick () {
    axios.post('http://localhost:50948/subdomain/findipaddresses', this.state.subDomains)
      .then(response => console.log(response))
      .catch(err => console.error("error: " + err)); 
  }

  render() {
    return (
      <div className="subdomain-app">
      <input  id="domain" 
              type="text" 
              placeholder="domain e,g yahoo.com" 
              value={this.state.domain}
              onChange={this.handleDomainChange}></input>
        <div className='button__container'>
          <button className='button' onClick={this.handleSubDomainClick}>List SubDomains</button>
          <button className='button' onClick={this.handleIPClick}>Find IP Addresses</button>
        </div>
        <table>
          <tbody>
        <tr>
            <th>#</th>
            <th>SubDomain</th> 
            <th>IP Addresses</th>
        </tr>
        {this.state.subDomainItems}
        </tbody>
        </table>
      </div>
    );
  }
}

export default App;
