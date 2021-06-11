import React from 'react';
import './App.css';
import Router from './router';
import 'antd/dist/antd.css';
import {Elements} from '@stripe/react-stripe-js';
import {loadStripe} from '@stripe/stripe-js';

const stripePromise = loadStripe('pk_test_WC7EYFzpuVCbs8ONcfSrjNE800zRXJe3lw');

const App = () => {
  return (
    <Elements stripe={stripePromise}>
      <Router />
    </Elements>
    
  );
}

export default App;
