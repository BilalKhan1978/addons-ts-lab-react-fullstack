import './App.css';
import {BrowserRouter,Route,Routes} from 'react-router-dom'
import ContactListing from './components/PuppyListing';
import CreateContacts from './components/CreatePuppy';
import EditContacts from './components/EditPuppy';

function App() {

  
  return (
  
    <div className="container"> 
    
    <BrowserRouter>
    <Routes>
      <Route path='/' element={<ContactListing/>}></Route>
      <Route path='/contacts/create' element={<CreateContacts/>}></Route>
      <Route path='/contacts/edit/:contactid' element={<EditContacts/>}></Route>
      
    </Routes>
  </BrowserRouter>
    </div>
 
 );
 }


export default App;
