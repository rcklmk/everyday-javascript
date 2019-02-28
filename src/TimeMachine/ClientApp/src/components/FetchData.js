import React, { Component } from 'react';
import styled from 'styled-components';
import { Service } from '../service';

let initialState = {
  isFetching: true,
  authenticated: false,
  headers: null,
  snapshot: null
};

// =============================================================================

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor (props) {
    super(props);
    Object.assign(this, {
      state: initialState,
      handleClick: this.handleClick.bind(this)
    });
  }

  async componentDidMount() {
    let res = await Service.fetchHeaders();

    if (res.redirected) {
      this.setState(_ => ({
        isFetching: false
      }));
      return;
    }

    else {
      let result = await res.json();
      this.setState(_ => ({
        isFetching: false,
        authenticated: true,
        headers: result
      }));
    }
  }

  async handleClick(e) {
    let id = parseInt(e.currentTarget.dataset.targetId);
    let jsonStr = await Service.fetchSnapshot(id).then(res => res.text());
    this.setState(_ => ({
      snapshot: jsonStr
    }));
    return;
  }

  render() {
    return React.createElement(View, {
      ...this.state,
      handleClick: this.handleClick
    });
  }
}

// =============================================================================

let View = props => {
  if (props.isFetching) {
    return (
      <div>
        Now loading...
      </div>
    );
  }

  if (!props.authenticated) {
    return (
      <div>
        Please login.
      </div>
    );
  }

  else {
    return (
      <div>
        <TableContainer>
          <table className="table">
            <thead>
              <tr>
                <th colSpan="2">
                  click me!
                </th>
              </tr>
            </thead>
            <tbody>
              {props.headers.map(header =>
                <ClickableRow
                  key={header.id}
                  onClick={props.handleClick}
                  data-target-id={header.id}>
                  <td>{header.id}</td>
                  <td>{header.timestamp}</td>
                </ClickableRow>)}
            </tbody>
          </table>
        </TableContainer>
        <SnapshotJsonViewer data={props.snapshot} />
      </div>
    );
  }
}

let TableContainer = styled.div`
  display: inline-block;
  width: 20rem;
`

let ClickableRow = styled.tr`
  color: black;

  &:hover {
    cursor: pointer;
    color: blue;
  }
`

// =============================================================================

let SnapshotJsonViewer = props => {
  if (props.data === null) {
    return null;
  }

  else {
    return (
      <JsonPanel>
        <code>
          {props.data}
        </code>
      </JsonPanel>
    );
  }
}

let JsonPanel = styled.div`
  float: right;
  clear: none;
  width: 500px;
`
