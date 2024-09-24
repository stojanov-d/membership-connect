/* eslint-disable @typescript-eslint/no-unused-vars */
import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { Button } from '@/components/ui/button';
import {
	Card,
	CardContent,
	CardDescription,
	CardFooter,
	CardHeader,
	CardTitle,
} from '@/components/ui/card';
import { AlertTriangle, Trash2 } from 'lucide-react';
import { membershipService, Membership } from '../api/membershipService';

export default function LandingPage() {
	const [memberships, setMemberships] = useState<Membership[]>([]);
	const [loading, setLoading] = useState(true);
	const [error, setError] = useState<string | null>(null);

	const fetchMemberships = async () => {
		try {
			const data = await membershipService.getMemberships();
			setMemberships(data);
			setLoading(false);
		} catch (err) {
			setError(
				'Failed to fetch memberships. Please check your API configuration..'
			);
			setLoading(false);
		}
	};

	useEffect(() => {
		fetchMemberships();
	}, []);

	const handleDelete = async (id: number) => {
		try {
			await membershipService.deleteMembership(id);
			fetchMemberships();
		} catch (error) {
			console.error('Failed to delete membership:', error);
		}
	};

	if (loading)
		return <div className="container mx-auto px-4 py-8">Loading...</div>;
	if (error)
		return (
			<div className="container mx-auto px-4 py-8">
				<div
					className="bg-yellow-100 border-l-4 border-yellow-500 text-yellow-700 p-4"
					role="alert"
				>
					<div className="flex">
						<div className="py-1">
							<AlertTriangle className="h-6 w-6 text-yellow-500 mr-4" />
						</div>
						<div>
							<p className="font-bold">Error</p>
							<p>{error}</p>
							<p>
								API Base URL: {import.meta.env.VITE_API_BASE_URL || 'Not set'}
							</p>
						</div>
					</div>
				</div>
			</div>
		);

	return (
		<div className="flex flex-col min-h-screen w-full bg-cyan-700">
			<header className="w-full px-4 lg:px-6 h-14 flex items-center bg-white shadow-sm">
				<Link to="/" className="flex items-center justify-center">
					<span className="font-bold text-2xl text-primary">
						Membership Connect
					</span>
				</Link>
				<nav className="ml-auto flex gap-4 sm:gap-6">
					<Link to="/" className="text-sm font-medium hover:text-primary">
						Home
					</Link>
					<Link
						to="/add-membership"
						className="text-sm font-medium hover:text-primary"
					>
						Add Membership
					</Link>
				</nav>
			</header>
			<main className="flex-1 w-full">
				<section className="w-full py-12 md:py-24 lg:py-32 xl:py-48 bg-primary">
					<div className="container mx-auto px-4 md:px-6">
						<div className="flex flex-col items-center space-y-4 text-center">
							<div className="space-y-2">
								<h1 className="text-3xl font-bold tracking-tighter sm:text-4xl md:text-5xl lg:text-6xl/none text-white">
									Welcome to Membership Connect
								</h1>
								<p className="mx-auto max-w-[700px] text-gray-200 md:text-xl">
									Simplify your membership management and grow your community
									with our powerful platform.
								</p>
							</div>
							<div className="space-x-4">
								<Button variant="secondary">Get Started</Button>
								<Button
									variant="outline"
									className="bg-white text-primary hover:bg-gray-100"
								>
									Learn More
								</Button>
							</div>
						</div>
					</div>
				</section>
				<section className="w-full py-12 md:py-24 lg:py-32 bg-white">
					<div className="container mx-auto px-4 md:px-6">
						<h2 className="text-3xl font-bold tracking-tighter sm:text-4xl md:text-5xl text-center mb-12 text-primary">
							Choose Your Membership
						</h2>
						<div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
							{memberships.map((membership) => (
								<Card
									key={membership.id}
									className="flex flex-col justify-between"
								>
									<CardHeader>
										<CardTitle>{membership.name}</CardTitle>
										<CardDescription>{membership.description}</CardDescription>
									</CardHeader>
									<CardContent>
										<p className="text-4xl font-bold mb-4 text-primary">
											${membership.price}
										</p>
										<p className="text-sm text-gray-500">
											Duration: {membership.durationInMonths} months
										</p>
									</CardContent>
									<CardFooter className="flex justify-between">
										<Link to={`/membership/${membership.id}`}>
											<Button>View Details</Button>
										</Link>
										<Button
											variant="destructive"
											onClick={() => handleDelete(membership.id)}
										>
											<Trash2 className="mr-2 h-4 w-4" /> Delete
										</Button>
									</CardFooter>
								</Card>
							))}
						</div>
					</div>
				</section>
			</main>
			<footer className="w-full flex flex-col gap-2 sm:flex-row py-6 shrink-0 items-center px-4 md:px-6 border-t bg-white">
				<p className="text-xs text-gray-500">
					© 2023 Membership Connect. All rights reserved.
				</p>
				<nav className="sm:ml-auto flex gap-4 sm:gap-6">
					<Link to="#" className="text-xs hover:text-primary">
						Terms of Service
					</Link>
					<Link to="#" className="text-xs hover:text-primary">
						Privacy
					</Link>
				</nav>
			</footer>
		</div>
	);
}
