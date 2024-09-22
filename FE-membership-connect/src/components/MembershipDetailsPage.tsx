/* eslint-disable @typescript-eslint/no-unused-vars */
import { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { Button } from '@/components/ui/button';
import {
	Card,
	CardContent,
	CardDescription,
	CardHeader,
	CardTitle,
} from '@/components/ui/card';
import { membershipService, Membership } from '../api/membershipService';

export default function MembershipDetailsPage() {
	const { id } = useParams<{ id: string }>();
	const navigate = useNavigate();
	const [membership, setMembership] = useState<Membership | null>(null);
	const [loading, setLoading] = useState(true);
	const [error, setError] = useState<string | null>(null);

	useEffect(() => {
		const fetchMembership = async () => {
			if (!id) return;
			try {
				const data = await membershipService.getMembershipById(
					parseInt(id, 10)
				);
				setMembership(data);
				setLoading(false);
			} catch (err) {
				setError(
					'Failed to fetch membership details. Please check your API configuration.'
				);
				setLoading(false);
			}
		};

		fetchMembership();
	}, [id]);

	if (loading)
		return <div className="container mx-auto px-4 py-8">Loading...</div>;
	if (error)
		return (
			<div className="container mx-auto px-4 py-8">
				<div
					className="bg-yellow-100 border-l-4 border-yellow-500 text-yellow-700 p-4"
					role="alert"
				>
					<p className="font-bold">Error</p>
					<p>{error}</p>
				</div>
			</div>
		);
	if (!membership)
		return (
			<div className="container mx-auto px-4 py-8">Membership not found</div>
		);

	return (
		<div className="container mx-auto px-4 py-8">
			<Button onClick={() => navigate('/')} className="mb-4">
				Back to Home
			</Button>
			<Card className="w-full max-w-2xl mx-auto">
				<CardHeader>
					<CardTitle className="text-3xl">{membership.name}</CardTitle>
					<CardDescription>{membership.description}</CardDescription>
				</CardHeader>
				<CardContent>
					<p className="text-4xl font-bold mb-4">${membership.price}</p>
					<p className="text-lg">
						Duration: {membership.durationInMonths} months
					</p>
				</CardContent>
			</Card>
		</div>
	);
}
