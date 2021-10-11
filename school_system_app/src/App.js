import './App.css';
import { BrowserRouter, Route, Switch } from 'react-router-dom';

import { NavigationBar } from './Components/HeaderComp/NavigationBar';
import {Students} from './Components/BodyComp/Students';
import {Student} from './Components/BodyComp/Student';
import {Admission} from './Components/BodyComp/Admission';
import {Toppers} from './Components/BodyComp/Toppers';
import { Class } from './Components/BodyComp/Class';
import Home from './Components/BodyComp/Home';

function App() {
  return (

    <BrowserRouter>
      <div className="App">
        <NavigationBar />
        <Switch>
        <Route path='/' component={Home} exact/>
        <Route path='/Students' component={Students} exact/>
        <Route path='/Student' component={Student} exact/>
        <Route path='/Admission' component={Admission} exact/>
        <Route path='/Toppers' component={Toppers} exact/>
        <Route path='/Class' component={Class} exact/>


        </Switch>
      </div>
    </BrowserRouter>
  );
}

export default App;
