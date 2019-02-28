
let base = 'api';

export let Service = {
  register: form => fetch(`${base}/auth/register`, {
    method: 'POST',
    headers: {'Content-Type': 'application/json'},
    body: JSON.stringify(form)
  }),

  login: form => fetch(`${base}/auth/login`, {
    method: 'POST',
    headers: {'Content-Type': 'application/json'},
    body: JSON.stringify(form)
  }),

  logout: _ => fetch(`${base}/auth/logout`, {
    method: 'POST',
  }),

  fetchHeaders: _ => fetch(`${base}/snapshot/all/headers`),
  fetchSnapshot: id => fetch(`${base}/snapshot/${id}`)
};
