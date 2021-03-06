import React, { Component } from 'react';
import './App.css';
import axios from 'axios'

class App extends Component {
  constructor () {
    super() 
    this.state = { 
      domain: 'yahoo.com.au',
      subDomains: [],
      subDomainItems: ''
    }
    this.handleSubDomainClick = this.handleSubDomainClick.bind(this)
    this.handleIPClick = this.handleIPClick.bind(this)
    this.handleDomainChange = this.handleDomainChange.bind(this)
    this.bindSubDomainData = this.bindSubDomainData.bind(this)
    this.bindIPData = this.bindIPData.bind(this)
  }

  handleDomainChange(event) {
    this.setState({domain: event.target.value})
    this.setState({subDomains: []})
    this.setState({subDomainItems: ''})
  }
  
  bindIPData(data) {
    if (data !== undefined)
    {
      this.setState({subDomainItems: data.map((item, index) =>
        <tr key={index}><td>{index}</td><td>{item.hostName}</td>{item.subDomains != null ? item.subDomains.join() : '-'}<td></td></tr>
        )});
      }
    }

  bindSubDomainData(data) {
    if (data !== undefined)
    {
      this.setState({subDomains: data});
      this.setState({subDomainItems: data.map((item, index) =>
        <tr key={index}><td>{index}</td><td>{item}</td>&nbsp;<td></td></tr>
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
    // if (this.state.subDomain !== undefined)
    // {
      axios('http://localhost:50948/subdomain/findipaddresses', {
        method: 'POST',
        mode: 'cors',
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json',
        },
        withCredentials: false,
        data: JSON.stringify(this.state.subDomains),
      }).then(response => { this.bindIPData(response.data) })
      .catch(err => console.error("error: " + err));  
    // }
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
