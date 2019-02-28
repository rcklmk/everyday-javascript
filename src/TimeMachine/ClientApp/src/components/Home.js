import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <h1>Hello, world!</h1>
        <p>
          This app take snapshots of <code>/r/javascript.json</code> subreddit every hour (XX:00).
        </p>
      </div>
    );
  }
}
