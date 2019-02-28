import React from 'react';
import styled from 'styled-components';

import { Service } from '../service';

// =============================================================================

class Container extends React.Component {
  constructor(props) {
    super(props);
    Object.assign(this, {
      state: {
        emailAddress: '',
        password: ''
      },

      handleChangeText: this.handleChangeText.bind(this),
      handleSubmit: this.handleSubmit.bind(this),
      handleRegister: this.handleRegister.bind(this)
    });
  }

  handleChangeText(e) {
    let {name: stateName, value} = e.target;
    this.setState(_ => ({
      [stateName]: value
    }));
  }

  async handleRegister(e) {
    let form = this.state;
    let text = await Service.register(form).then(res => res.text());
    window.alert(text);
  }

  async handleSubmit(e) {
    e.preventDefault();
    let form = this.state;

    let res = await Service.login(form);
    
    window.alert(`[${res.statusText}]`);
  }

  async handleLogout(_) {
    let text = await Service.logout().then(res => res.text());
    window.alert(text);
    return;
  }

  render() {
    return React.createElement(View, {
      ...this.state,
      handleChangeText: this.handleChangeText,
      handleSubmit: this.handleSubmit,
      handleRegister: this.handleRegister,
      handleLogout: this.handleLogout
    });
  }
}

// =============================================================================

let View = props =>
  <FormContainer>
    <form onSubmit={props.handleSubmit}>
      <div className="input-group mb-3">
        <input
          className="form-control"
          type="text"
          name="emailAddress"
          placeholder="[valid-email-address]"
          value={props.emailAddress}
          onChange={props.handleChangeText}
        />
      </div>
      <div className="input-group mb-3">
        <input
          className="form-control"
          type="password"
          name="password"
          placeholder="[valid-password]"
          value={props.password}
          onChange={props.handleChangeText}
        />
      </div>
      <div className="input-group mb3">
        <input className="btn btn-primary" type="submit" value="Login" />
      </div>
    </form>
    <hr />
    <button
      className="btn btn-dark"
      onClick={props.handleRegister}>Register</button>
    &nbsp;
    <button
      className="btn btn-info"
      onClick={props.handleLogout}>Logout</button>
    <hr />
    <div>
      <b>Valid password:</b>
      <ul>
        <li>required length: 8</li>
        <li>unique chars: 6</li>
        <li>contains uppercase</li>
        <li>contains digit</li>
      </ul>
      <span>e.g. 「1234Abcd」</span>
    </div>
  </FormContainer>

let FormContainer = styled.div`
  width: 17rem;
  margin-left: auto;
  margin-right: auto;
`

export let Account = Container;