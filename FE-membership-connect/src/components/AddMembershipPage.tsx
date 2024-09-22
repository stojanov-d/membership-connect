import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { Textarea } from '@/components/ui/textarea';
import { membershipService } from '../api/membershipService';
import { AlertCircle, CheckCircle } from 'lucide-react';

export default function AddMembershipPage() {
	const navigate = useNavigate();
	const [name, setName] = useState('');
	const [price, setPrice] = useState('');
	const [description, setDescription] = useState('');
	const [durationInMonths, setDurationInMonths] = useState('');
	const [loading, setLoading] = useState(false);
	const [error, setError] = useState<string | null>(null);
	const [success, setSuccess] = useState<string | null>(null);

	const handleSubmit = async (e: React.FormEvent) => {
		e.preventDefault();
		setLoading(true);
		setError(null);
		setSuccess(null);

		try {
			const result = await membershipService.addMembership({
				name,
				price: parseFloat(price),
				description,
				durationInMonths: parseInt(durationInMonths, 10),
			});
			setSuccess(result);
			// Clear the form
			setName('');
			setPrice('');
			setDescription('');
			setDurationInMonths('');
			// Redirect to the landing page after a short delay
			setTimeout(() => navigate('/'), 1000);
		} catch (error) {
			console.error('Failed to add membership:', error);
			setError('Failed to add membership. Please try again.');
		} finally {
			setLoading(false);
		}
	};

	return (
		<div className="container mx-auto px-4 py-8 w-full">
			<h1 className="text-2xl font-bold mb-4">Add New Membership</h1>
			{error && (
				<div
					className="bg-red-100 border-l-4 border-red-500 text-red-700 p-4 mb-4"
					role="alert"
				>
					<div className="flex">
						<div className="py-1">
							<AlertCircle className="h-6 w-6 text-red-500 mr-4" />
						</div>
						<div>
							<p className="font-bold">Error</p>
							<p>{error}</p>
						</div>
					</div>
				</div>
			)}
			{success && (
				<div
					className="bg-green-100 border-l-4 border-green-500 text-green-700 p-4 mb-4"
					role="alert"
				>
					<div className="flex">
						<div className="py-1">
							<CheckCircle className="h-6 w-6 text-green-500 mr-4" />
						</div>
						<div>
							<p className="font-bold">Success</p>
							<p>{success}</p>
							<p>Redirecting to home page...</p>
						</div>
					</div>
				</div>
			)}
			<form onSubmit={handleSubmit} className="space-y-4 max-w-md mx-auto">
				<div>
					<label
						htmlFor="name"
						className="block text-sm font-medium text-gray-700"
					>
						Name
					</label>
					<Input
						id="name"
						value={name}
						onChange={(e) => setName(e.target.value)}
						required
					/>
				</div>
				<div>
					<label
						htmlFor="price"
						className="block text-sm font-medium text-gray-700"
					>
						Price
					</label>
					<Input
						id="price"
						type="number"
						step="0.01"
						value={price}
						onChange={(e) => setPrice(e.target.value)}
						required
					/>
				</div>
				<div>
					<label
						htmlFor="description"
						className="block text-sm font-medium text-gray-700"
					>
						Description
					</label>
					<Textarea
						id="description"
						value={description}
						onChange={(e) => setDescription(e.target.value)}
						required
					/>
				</div>
				<div>
					<label
						htmlFor="durationInMonths"
						className="block text-sm font-medium text-gray-700"
					>
						Duration (months)
					</label>
					<Input
						id="durationInMonths"
						type="number"
						value={durationInMonths}
						onChange={(e) => setDurationInMonths(e.target.value)}
						required
					/>
				</div>
				<Button type="submit" className="w-full" disabled={loading}>
					{loading ? 'Adding...' : 'Add Membership'}
				</Button>
			</form>
		</div>
	);
}
