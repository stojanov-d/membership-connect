import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import LandingPage from './components/LandingPage';
import AddMembershipPage from './components/AddMembershipPage';
import MembershipDetailsPage from './components/MembershipDetailsPage';

export default function App() {
	return (
		<Router>
			<Routes>
				<Route path="/" element={<LandingPage />} />
				<Route path="/add-membership" element={<AddMembershipPage />} />
				<Route path="/membership/:id" element={<MembershipDetailsPage />} />
			</Routes>
		</Router>
	);
}
